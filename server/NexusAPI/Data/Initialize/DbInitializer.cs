﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Data;
using NexusAPI.Models;

namespace NexusAPI.Data.Initialize
{
    public class DbInitializer : IDbInitializer
    {

        private readonly ApplicationDbContext _db;

        public DbInitializer(

            ApplicationDbContext db)
        {
            _db = db;
        }


        public void Initialize(bool e)
        {

            //migrations if they are not applied
            try
            {
                if (e)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }


            //migrations if they are not applied
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }
         




            return;
        }
    }
}
