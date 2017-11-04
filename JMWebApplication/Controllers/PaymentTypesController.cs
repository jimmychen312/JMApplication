using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JMEFDataAccess;
using JMModels;
using System.Web.Security;
using JM.Filters;

namespace JMApplication.Controllers
{
    public class PaymentTypesController : ApiController
    {
        private JMDatabase db = new JMDatabase();

        
        // GET: api/PaymentTypes
        public IQueryable<PaymentType> GetPaymentTypes()
        {
            return db.PaymentTypes;
        }

        // GET: api/PaymentTypes/5
        [ResponseType(typeof(PaymentType))]
        public IHttpActionResult GetPaymentType(Guid id)
        {
            PaymentType paymentType = db.PaymentTypes.Find(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            return Ok(paymentType);
        }

        // PUT: api/PaymentTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPaymentType(Guid id, PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != paymentType.PaymentTypeID)
            {
                return BadRequest();
            }

            db.Entry(paymentType).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PaymentTypes
        [ResponseType(typeof(PaymentType))]
        public IHttpActionResult PostPaymentType(PaymentType paymentType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PaymentTypes.Add(paymentType);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PaymentTypeExists(paymentType.PaymentTypeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = paymentType.PaymentTypeID }, paymentType);
        }

        // DELETE: api/PaymentTypes/5
        [ResponseType(typeof(PaymentType))]
        public IHttpActionResult DeletePaymentType(Guid id)
        {
            PaymentType paymentType = db.PaymentTypes.Find(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            db.PaymentTypes.Remove(paymentType);
            db.SaveChanges();

            return Ok(paymentType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PaymentTypeExists(Guid id)
        {
            return db.PaymentTypes.Count(e => e.PaymentTypeID == id) > 0;
        }
    }
}