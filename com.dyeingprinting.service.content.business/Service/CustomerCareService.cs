using com.dyeingprinting.service.content.data;
using com.dyeingprinting.service.content.data.Model;
using EWorkplaceAbsensiService.Lib.Helpers.IdentityService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Moonlay.Models;

namespace com.dyeingprinting.service.content.business.Service
{
    public class CustomerCareService : IService<CustomerCare>
    {
        private readonly ContentDbContext _context;
        private readonly DbSet<CustomerCare> _customerCareContentDbset;
        private readonly IIdentityService _identityService;
        private const string USER_AGENT = "Core Service";
        private const string USER_BY = "Core Service";
        public CustomerCareService(ContentDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _customerCareContentDbset = context.Set<CustomerCare>();
            _identityService = serviceProvider.GetService<IIdentityService>();
        }

        public Task<int> Create(CustomerCare model)
        {
            _customerCareContentDbset.Add(model);
            return _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            var model = await GetSingleById(id);

            if (model == null)
                throw new Exception("Invalid Id");

            EntityExtension.FlagForDelete(model, USER_BY,USER_AGENT);
            _customerCareContentDbset.Update(model);
            return await _context.SaveChangesAsync();
        }

        public List<CustomerCare> Find()
        {
            return _context.CustomerCare.ToList();
        }

        public Task<List<CustomerCare>> FindAsync()
        {
            return _context.CustomerCare.ToListAsync();
        }
            
        public Task<CustomerCare> GetSingleById(int id)
        {
            return _customerCareContentDbset.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<int> Update(CustomerCare dbmodel, CustomerCare model)
        {
            dbmodel.Title = model.Title;
            dbmodel.Description = model.Description;
            dbmodel.ImageUrl1 = model.ImageUrl1;
            dbmodel.ContentTitle1 = model.ContentTitle1;
            dbmodel.ContentDescription1 = model.ContentDescription1;
            dbmodel.TextButton1 = model.TextButton1;
            dbmodel.ContentUrl1 = model.ContentUrl1;
            dbmodel.ImageUrl2 = model.ImageUrl2;
            dbmodel.ContentTitle2 = model.ContentTitle2;
            dbmodel.ContentDescription2 = model.ContentDescription2;
            dbmodel.TextButton2 = model.TextButton2;
            dbmodel.ContentUrl2 = model.ContentUrl2;

            return _context.SaveChangesAsync();
        }
    }
}
