using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashBoard.Model.Models;

namespace DashBoard.DAL.EF
{
    public class CreateDbIfNot : CreateDatabaseIfNotExists<DutContext>
    {
        protected override void Seed(DutContext context)
        {
            context.SeedAdminAccount("romeost", "123456");
            context.Institutes.AddRange(new List<Institute>
            {
                new Institute {Name = "Телекомунікацій та інформатизації"},
                new Institute {Name = "Захисту інформації", Departments = new List<Department>
                {
                    new Department {Name = "Інформаційної та кібернетичної безпеки "},
                    new Department {Name = "Управління інформаційною та кібернетичною безпекою"},
                    new Department {Name = "Систем інформаційного та кібернетичного захисту"}
                }},
                new Institute {Name = "Менеджменту та підприємництва", Departments = new List<Department>
                {
                    new Department {Name = "Менеджменту"},
                    new Department {Name = "Маркетингу"},
                    new Department {Name = "Підприємництва, торгівлі та біржової діяльності"},
                    new Department {Name = "Економіки підприємств та соціальних технологій"},
                    new Department {Name = "Документознавства та інформаційної діяльності"},
                    new Department {Name = "Публичного управління та адміністрування"}
                }},
                new Institute {Name = "Заочного та Дистанційного навчання"},
                new Institute {Name = "Післядипломної освіти"},
                new Institute {Name = "Гуманітарних та природничих дисциплін", Departments = new List<Department>
                {
                    new Department {Name = "Вищої математики"},
                    new Department {Name = "Іноземних мов"},
                    new Department {Name = "Фізики"},
                    new Department {Name = "Фізичної культури та охорони праці"}
                }}
            });

            context.SaveChanges();

            context.Faculties.AddRange(new List<Faculty>
            {
                new Faculty {Name = "Інформаційних технологій", Departments = new List<Department>
                {
                    new Department {Name = "Комп'ютерних наук"},
                    new Department {Name = "Інформаційних систем та технологій"},
                    new Department {Name = "Комп'ютерної інженерії"},
                    new Department {Name = "Системного аналізу"},
                    new Department {Name = "Інженерії програмного забезпечення"}
                }},
                new Faculty {Name = "Телекомунікацій", Departments = new List<Department>
                {
                    new Department {Name = "Енергоефективних технологій"},
                    new Department {Name = "Космічних систем та комплексів і супутникових телекомунікацій"},
                    new Department {Name = "Мобільних та відеоінформаційних технологій"},
                    new Department {Name = "Телекомунікаційних систем"},
                    new Department {Name = "Телекомунікаційних технологій"}
                }}
            });

            context.SaveChanges();
        }
    }
}
