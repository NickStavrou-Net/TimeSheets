﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Timesheets.Areas.Identity.Data;
using Timesheets.Data;

namespace Timesheets.Models
{
    public class SeedData
    {
        // check: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-3.0&tabs=visual-studio
        // and: http://www.binaryintellect.net/articles/5e180dfa-4438-45d8-ac78-c7cc11735791.aspx

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var username1 = "petroula@a.a";
            if (userManager.FindByNameAsync(username1).Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = username1,
                    Email = username1,
                    FirstName = "Petroula",
                    LastName = "Stamouli",
                    ManHourCost = 3.14,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "12345^qW").Result;
                //if (result.Succeeded)
                //{
                //    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                //}
            }
            var username2 = "sofia@b.b";
            if (userManager.FindByNameAsync(username2).Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = username2,
                    Email = username2,
                    FirstName = "Sofia",
                    LastName = "Tseranidou",
                    ManHourCost = 5,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "12345^qW").Result;
                //if (result.Succeeded)
                //{
                //    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                //}
            }
            var username3 = "nikos@c.c";
            if (userManager.FindByNameAsync(username3).Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = username3,
                    Email = username3,
                    FirstName = "Nikos",
                    LastName = "Stavrou",
                    ManHourCost = 4,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "12345^qW").Result;
                //if (result.Succeeded)
                //{
                //    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                //}
            }
            var username4 = "antonis@d.d";
            if (userManager.FindByNameAsync(username4).Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = username4,
                    Email = username4,
                    FirstName = "Antonis",
                    LastName = "Fragkiadakis",
                    ManHourCost = 2,
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "12345^qW").Result;
                //if (result.Succeeded)
                //{
                //    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                //}
            }
        }

        public static void SeedRest(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Departments.Any())
                {
                    return;   // DB has been seeded
                }

                var usersIdList = context.ApplicationUsers.Select(x => x.Id).Distinct().ToList();

                IList<Department> newDepartments = new List<Department>()
                {
                    new Department() { Name = "Banking", DepartmentHeadId = usersIdList[0] },
                    new Department() { Name = "Infrastructure", DepartmentHeadId = usersIdList[2] },
                    new Department() { Name = "Networking", DepartmentHeadId = usersIdList[1] },
                    new Department() { Name = "Telecommunications", DepartmentHeadId = usersIdList[2] },
                    new Department() { Name = "Finance", DepartmentHeadId = usersIdList[1] }

                };
                context.Departments.AddRange(newDepartments);
                context.SaveChanges();

                var newDepartmentsIdList = context.Departments.Select(x => x.DepartmentId).Distinct().ToList();

                IList<Project> newProjects = new List<Project>()
                {
                    new Project() { Name = "Android App For New Product", OwnerDepartmentId = newDepartmentsIdList[0] },
                    new Project() { Name = "New Website", OwnerDepartmentId = newDepartmentsIdList[1] },
                    new Project() { Name = "New database", OwnerDepartmentId = newDepartmentsIdList[0] },
                    new Project() { Name = "Create a website for the product.", OwnerDepartmentId = newDepartmentsIdList[2] },
                    new Project() { Name = "Integrate sql db.", OwnerDepartmentId = newDepartmentsIdList[1] }

                };
                context.Projects.AddRange(newProjects);
                context.SaveChanges();

                var newProjectsIdList = context.Projects.Select(x => x.ProjectId).Distinct().ToList();

                IList<DepartmentProject> newDPs = new List<DepartmentProject>()
                {
                    new DepartmentProject() { DepartmentId = newDepartmentsIdList[0], ProjectId = newProjectsIdList[0] },
                    new DepartmentProject() { DepartmentId = newDepartmentsIdList[1], ProjectId = newProjectsIdList[0] },
                    new DepartmentProject() { DepartmentId = newDepartmentsIdList[1], ProjectId = newProjectsIdList[1] },
                    new DepartmentProject() { DepartmentId = newDepartmentsIdList[0], ProjectId = newProjectsIdList[2] },
                    new DepartmentProject() { DepartmentId = newDepartmentsIdList[1], ProjectId = newProjectsIdList[2] },
                    new DepartmentProject() { DepartmentId = newDepartmentsIdList[2], ProjectId = newProjectsIdList[2] },
                    new DepartmentProject() { DepartmentId = newDepartmentsIdList[0], ProjectId = newProjectsIdList[3] },
                    new DepartmentProject() { DepartmentId = newDepartmentsIdList[2], ProjectId = newProjectsIdList[3] }
                };
                context.DepartmentProjects.AddRange(newDPs);
                context.SaveChanges();

                IList<TimesheetEntry> newTimesheetEntries = new List<TimesheetEntry>()
                {
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 7, UserId = usersIdList[0], ProjectId = newProjectsIdList[0] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 3, UserId = usersIdList[0], ProjectId = newProjectsIdList[1] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 8, UserId = usersIdList[2], ProjectId = newProjectsIdList[3] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 10,UserId = usersIdList[1], ProjectId = newProjectsIdList[2] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 4,UserId = usersIdList[2], ProjectId = newProjectsIdList[3] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 9,UserId = usersIdList[1], ProjectId = newProjectsIdList[1] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 12,UserId = usersIdList[1], ProjectId = newProjectsIdList[2] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 13,UserId = usersIdList[0], ProjectId = newProjectsIdList[1] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 14,UserId = usersIdList[0], ProjectId = newProjectsIdList[2] },
                    new TimesheetEntry() { DateCreated = DateTime.Now, HoursWorked = 20,UserId = usersIdList[1], ProjectId = newProjectsIdList[3] }
                };
                context.TimesheetEntries.AddRange(newTimesheetEntries);
              
                context.SaveChanges();

                // TODO: insert more data; update user DepartmentIds
            }
        }
    }
}
