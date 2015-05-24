using System;
using System.Collections.Generic;
using ContactsBackEnd.DATA.Entities;
using ContactsBackEnd.DATA.Repositories;
using Microsoft.AspNet.Mvc;

namespace ContactsBackEnd.WEBAPI.Controllers
{
    [Route("api/[Controller]/")]
    public class ContactsController : Controller
    {
        private readonly IContactsRepository _repo;

        public ContactsController(IContactsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<Contact> Get(string query = "", int page = 0, int pageSize = 20)
        {
            try
            {
                return _repo.GetAllContacts(query, page, pageSize);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        [HttpGet]
        [Route("{id}", Name = "GetByIdRoute")]
        public object Get(string id)
        {
            try
            {
                var contactId = Convert.ToInt32(id);

                var myEntity = _repo.GetContactById(contactId);

                return myEntity;
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
                _repo.Insert(contact);
                var url = Url.RouteUrl("GetByIdRoute", new { id = contact.Id}, Request.Scheme, Request.Host.ToUriComponent());
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
            catch (Exception ex)
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
    }
}
