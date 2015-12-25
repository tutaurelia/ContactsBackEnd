using System;
using ContactsBackEnd.Entities;
using ContactsBackEnd.Repositories;
using Microsoft.AspNet.Cors;
using Microsoft.AspNet.Mvc;

namespace ContactsBackEnd.Controllers
{
    [Route("api/[Controller]/")]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _repo;

        public ContactsController(IContactsRepository repo)
        {
            _repo = repo;
        }

     
        [HttpGet]
        // ReSharper disable once MethodOverloadWithOptionalParameter
        public object Get(string query = "", int page = 0, int pageSize = 20)
        {
            try
            {
                var totalCount = _repo.GetNumberOfContacts(query);
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                var contacts = _repo.GetAllContacts(query, page, pageSize);

                return new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Contacts = contacts
                };
            }
            catch (Exception ex)
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

                var myContact = _repo.GetContactById(contactId);

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
                var contactId = _repo.Insert(contact);
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

                var originalContact = _repo.GetContactById(contactId);
                if (originalContact == null) return HttpNotFound();

                _repo.Update(contactId, contact);

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

                if (_repo.GetContactById(contactId) == null) return HttpNotFound();
                _repo.Delete(contactId);
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
                _repo.ResetDataBase10Contacts();
                return new HttpStatusCodeResult(200);
            }
            catch (Exception)
            {
                return HttpBadRequest();
            }
        }



    }
}
