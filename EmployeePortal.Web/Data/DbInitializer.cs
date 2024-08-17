using EmployeePortal.Web.Models.Entities;

namespace EmployeePortal.Web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EFDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Cities.Any())
            {
                return;
            }

            var city = new City[]
            {
                new City() { Name = "Москва" },
                new City() { Name = "Санкт-Петербург" },
                new City() { Name = "Новосибирск" },
                new City() { Name = "Екатеринбург" },
                new City() { Name = "Нижний Новгород" },
                new City() { Name = "Казань" },
                new City() { Name = "Челябинск" },
                new City() { Name = "Омск" },
                new City() { Name = "Самара" },
                new City() { Name = "Ростов-на-Дону" },
                new City() { Name = "Красноярск" },
                new City() { Name = "Уфа" },
                new City() { Name = "Краснодар" },
                new City() { Name = "Воронеж" },
                new City() { Name = "Пермь" },
                new City() { Name = "Волгоград" }
            };
            foreach (City c in city)
            {
                context.Cities.Add(c);
            }
            context.SaveChanges();

            if (context.Employees.Any())
            {
                return;
            }

            var employee = new Employee[]
            {
                new Employee()
                {
                    Name = "Константин Грачев",
                    Code = "E-741234",
                    CityName = "Челябинск"
                },
                new Employee()
                {
                    Name = "Виктор Беглов",
                    Code = "М-521234",
                    CityName = "Нижний Новгород"
                }
            };
            foreach (Employee e in employee)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();
        }
    }
}
