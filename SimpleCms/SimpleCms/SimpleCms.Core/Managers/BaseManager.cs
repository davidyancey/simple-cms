using SimpleCms.Infrastructure;
using SimpleCms.Infrastructure.Entities;
using SimpleCms.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SimpleCms.Core.Managers
{
    public class BaseManager
    {

        private readonly ApplicationSourceEntities _context = new ApplicationSourceEntities();
        internal readonly Repository<ApplicationSourceEntities> _repository;
        private string _applicationName;

        internal BaseManager(string connectionName)
        {            
            _repository = new Repository<ApplicationSourceEntities>(_context,connectionName);
        }

        public Guid ApplicationId
        {
            get
            {
                return Guid.Parse(ConfigurationManager.AppSettings["ApplicationId"].ToString());
            }
        }
    }
}
