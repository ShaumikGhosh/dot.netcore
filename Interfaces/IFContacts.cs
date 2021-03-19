using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TelephoneApp.Data;
using TelephoneApp.Models;

namespace TelephoneApp.Interfaces
{
    public interface IFContacts
    {
        List<PhonebookModel> GetAll(ClaimsPrincipal User);

        PhonebookModel GetById(int? Id);

        void Insert(PhonebookModel model, ApplicationUser User);

        void Update(PhonebookModel model, int? Id);

        void Delete(int? Id);

        void Save();
    }
}
