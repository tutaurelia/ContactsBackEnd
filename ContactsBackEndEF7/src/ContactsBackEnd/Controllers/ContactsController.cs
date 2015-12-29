using ContactsBackEnd.Models;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactsBackEnd.Controllers
{
    [Route("api/[Controller]/")]
    public class ContactsController : Controller
    {
        [FromServices]
        public IContactsRepository Repo {get; set;}

       


        [HttpGet]
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public object Get(string query = "", int page = 0, int pageSize = 20)
        {
            try
            {
                var totalCount = Repo.GetNumberOfContacts(query);
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                var contacts = Repo.GetAllContacts(query, page, pageSize);

                return new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Contacts = contacts
                };
            }
            catch (Exception)
            {
                return HttpBadRequest();
            }
        }


        [HttpGet]
        [Route("{id}", Name = "GetContactByIdRoute")]
        public object Get(string id)
        {
            try
            {
                var contactId = Convert.ToInt32(id);

                var myContact = Repo.GetContactById(contactId);

                return myContact;
            }
            catch (Exception)
            {
                return HttpBadRequest();
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest();

            }
            try
            {
                var contactId = Repo.Insert(contact);
                var url = Url.RouteUrl("GetContactByIdRoute", new { id = contactId }, Request.Scheme, Request.Host.ToUriComponent());
                return Created(url, contact);

            }
            catch (Exception)
            {
                return HttpBadRequest();

            }
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(string id, [FromBody] Contact contact)
        {
            try
            {
                var contactId = Convert.ToInt32(id);

                if (!ModelState.IsValid)
                {
                    return HttpBadRequest();
                }

                var originalContact = Repo.GetContactById(contactId);
                if (originalContact == null) return HttpNotFound();

                Repo.Update(contactId, contact);

                return new ObjectResult(contact);
            }
            catch (Exception)
            {
                return HttpBadRequest();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                var contactId = Convert.ToInt32(id);

                if (Repo.GetContactById(contactId) == null) return HttpNotFound();
                Repo.Delete(contactId);
                return new HttpStatusCodeResult(200);
            }
            catch (Exception)
            {
                return HttpBadRequest();
            }
        }

        [HttpPost("reset")]
        public IActionResult ResetDb()
        {
            try
            {
                Repo.ResetDataBase10Contacts();
                return new HttpStatusCodeResult(200);
            }
            catch (Exception)
            {
                return HttpBadRequest();
            }
        }



    }
}
