using System;
using System.Collections.Generic;
using System.Text;
using Newser.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Newser.DAL
{
    public class DBInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            ApplicationUser user = new ApplicationUser()
            {
                UserName = "Admin",
                Id = "Admin"
            };
            IdentityRole role = new IdentityRole("admin");

            userManager.Create(user, "password");
            roleManager.Create(role);
            userManager.AddToRole(user.Id, role.Name);


            //List<ApplicationUser> users = new List<ApplicationUser>();
            ApplicationUser usernew;
            IdentityRole editor = new IdentityRole("editor");
            roleManager.Create(editor);

            for (int i = 0; i < 10; i++)
            {
                usernew = new ApplicationUser()
                {
                    UserName = "User" + i,
                    Id = "User" + i,
                };

                userManager.Create(usernew, "password");               
                userManager.AddToRole(usernew.Id, editor.Name);
            }


            List<Node> nodes = new List<Node>();
            for (int i = 0; i < 10; i++)
            {
                Node node = new Node()
                {
                    CreateDate = DateTime.Now,
                    //Visits = 0,
                    Title = "Заголовок новости " + i,
                    Body = GenerateSeedText(i),
                    User = user
                };
                context.Nodes.Add(node);

                nodes.Add(node);

                if (i > 4 && i < 12)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        Comment comment = new Comment()
                        {
                            Node = node,
                            Body = GenerateSeedTextReply(0, j),
                            CreateDate = DateTime.Now,
                            User = user
                        };
                        context.Comments.Add(comment);
                    }
                }
            }

            Tag t1 = new Tag() { Name = "Знания" };
            Tag t2 = new Tag() { Name = "Умения" };
            Tag t3 = new Tag() { Name = "Навыки" };
            Tag t4 = new Tag() { Name = "программирование" };
            Tag t5 = new Tag() { Name = "языки" };

            context.Tags.Add(t1);
            context.Tags.Add(t2);
            context.Tags.Add(t3);
            context.Tags.Add(t4);
            context.Tags.Add(t5);

            nodes[0].Tags.Add(t1);
            nodes[0].Tags.Add(t2);
            nodes[1].Tags.Add(t2);
            nodes[1].Tags.Add(t3);
            nodes[2].Tags.Add(t3);
            nodes[2].Tags.Add(t4);
            nodes[2].Tags.Add(t5);
            nodes[3].Tags.Add(t5);


            context.SaveChanges();
        }

        string GenerateSeedText(int i)
        {
            StringBuilder sb = new StringBuilder();
            // sb.Append("Message " + i+"\n");
            for (int j = 0; j < 20; j++)
            {
                for (int k = 0; k < 5; k++)
                {
                    sb.Append("текст новости ");
                    sb.Append(i);
                    sb.Append(" ");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }

        string GenerateSeedTextReply(int iNews, int iComment)
        {
            StringBuilder sb = new StringBuilder();
            // sb.Append("Message " + i+"\n");
            for (int j = 0; j < 10; j++)
            {
                for (int k = 0; k < 2; k++)
                {
                    sb.Append("текст комментария ");
                    sb.Append(iComment);
                    sb.Append(" ");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
