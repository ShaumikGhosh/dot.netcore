using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TelephoneApp.Data;
using TelephoneApp.Interfaces;
using TelephoneApp.Models;

namespace TelephoneApp.Services
{
    public class ContactServices : IFContacts
    {


        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationContext _applicationContext;




        public ContactServices(
            ApplicationContext applicationContext,
            UserManager<ApplicationUser> userManager)
        {
            _applicationContext = applicationContext;
            _userManager = userManager;
        }





        public void Delete(int? Id)
        {
            var contact = _applicationContext.PhonebookRecords.Find(Id);
            _applicationContext.Remove(contact);
        }




        public List<PhonebookModel> GetAll(ClaimsPrincipal User)
        {
            var user = _userManager.GetUserAsync(User);
            return _applicationContext.PhonebookRecords.Where(x => x.user == user.Result).ToList();
        }




        public PhonebookModel GetById(int? Id)
        {
            return _applicationContext.PhonebookRecords.Where(x => x.Id == Id).FirstOrDefault();
        }





        public void Insert(PhonebookModel phonebookModel, ApplicationUser user)
        {
            PhonebookModel pm = new PhonebookModel();
            pm.FullName = phonebookModel.FullName;
            pm.PhoneNumber = phonebookModel.PhoneNumber;
            pm.Address = phonebookModel.Address;
            pm.user = user;
            _applicationContext.PhonebookRecords.Add(pm);
        }





        public void Save()
        {
            _applicationContext.SaveChanges();
        }





        public void Update(PhonebookModel phonebookModel, int? Id)
        {
            var record = _applicationContext.PhonebookRecords.Find(Id);
            record.FullName = phonebookModel.FullName;
            record.PhoneNumber = phonebookModel.PhoneNumber;
            record.Address = phonebookModel.Address;
            _applicationContext.PhonebookRecords.Update(record);
        }




    }
}
