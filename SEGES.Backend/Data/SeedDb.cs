using Microsoft.EntityFrameworkCore;
using SEGES.Shared.Entities;
using Newtonsoft.Json;
using SEGES.Shared.Enums;
using SEGES.Backend.UnitsOfWork.Interfaces;


namespace SEGES.Backend
{
    public class SeedDb
    {
        private readonly DataContext _context;

        private readonly IUsersUnitOfWork _usersUnitOfWork;
        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
        }

        public string statusNameList = @"[
                ""Pendiente de aprobación"",
                ""En revisión"",
                ""Aprobado"",
                ""Rechazado"",
                ""Aprobación parcial"",
                ""Aprobación condicional"",
                ""Pendiente de revisión adicional"",
                ""En espera de confirmación"",
                ""En proceso de aprobación"",
                ""Aprobación pendiente de firma"",
                ""Aprobación finalizada"",
                ""Aprobación automática""
            ]";

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesFullAsync();
            await CheckCountriesAsync();
            await CheckStatesAsync();
            await CheckCitiesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "admin", "prueba", "admin@yopmail.com", "555555", "direcion prueba", UserType.Admin);
            await CheckDocTraceabilityType();
            await CheckHUApprobalStatus();
            await CheckHUPriority();
            await CheckHUPublicationStatus();
            await CheckHUStatus();
            await CheckProjectStatus();
            // await CheckRoles();
        }

        private async Task CheckRolesAsync()
        {
            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.ReqEngineer.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.ProjectManager.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.StakeHolder.ToString());
        }
        private async Task<UserApp> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Bello");
                city ??= await _context.Cities.FirstOrDefaultAsync();

                user = new UserApp
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    City = city,
                    UserType = userType,
                };

                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());

                var token = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usersUnitOfWork.ConfirmEmailAsync(user, token);
            }

            return user;
        }


        private async Task CheckCountriesFullAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesStatesCitiesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesStatesCitiesSQLScript);
            }
        }
        /*
        private async Task CheckRoles()
        {
            if (!_context.Roles.Any())
            {
                //_context.ResetTableIDs("Roles");
                _context.Roles.Add(new Role { RoleName = "Administrador del sistema" });
                _context.Roles.Add(new Role { RoleName = "Gerente de proyecto" });
                _context.Roles.Add(new Role { RoleName = "Líder de equipo" });
                _context.Roles.Add(new Role { RoleName = "Desarrollador" });
                _context.Roles.Add(new Role { RoleName = "Tester/QA" });
                _context.Roles.Add(new Role { RoleName = "Analista de negocio" });
                _context.Roles.Add(new Role { RoleName = "Diseñador UX/UI" });
                _context.Roles.Add(new Role { RoleName = "Cliente" });
                _context.Roles.Add(new Role { RoleName = "Interesado" });
                _context.Roles.Add(new Role { RoleName = "Consultor" });
                _context.Roles.Add(new Role { RoleName = "Coordinador de proyecto" });
                _context.Roles.Add(new Role { RoleName = "Scrum Master" });
                _context.Roles.Add(new Role { RoleName = "Product Owner" });
                _context.Roles.Add(new Role { RoleName = "Analista de datos" });
                _context.Roles.Add(new Role { RoleName = "Arquitecto de software" });
                //SaveDBChanges();
                _context.SaveChanges();
            }
        }
        */
        private async Task CheckProjectStatus()
        {
            if (!_context.ProjectStatuses.Any())
            {
                var projectStatuses = JsonConvert.DeserializeObject<List<string>>(statusNameList);
                foreach (var item in projectStatuses)
                {
                    _context.ProjectStatuses.Add(new ProjectStatus { Name = item });
                }
                _context.SaveChanges();
            }
        }

        private async Task CheckHUStatus()
        {
            if (!_context.HUStatuses.Any())
            {
                var huStatuses = JsonConvert.DeserializeObject<List<string>>(statusNameList);
                foreach (var item in huStatuses)
                {
                    _context.HUStatuses.Add(new HUStatus { Name = item });
                }
                _context.SaveChanges();
            }
        }

        private async Task CheckHUPublicationStatus()
        {
            if (!_context.HUPublicationStatuses.Any())
            {
                //_context.ResetTableIDs("HUPublicationStatuses");

                var huPublicationStatuses = JsonConvert.DeserializeObject<List<string>>(statusNameList);
                foreach (var item in huPublicationStatuses)
                {
                    _context.HUPublicationStatuses.Add(new HUPublicationStatus { Name = item });
                }
                _context.SaveChanges();
            }
        }

        private async Task CheckHUPriority()
        {
            if (!_context.HUPriorities.Any())
            {
                //_context.ResetTableIDs("HUPriorities");
                _context.HUPriorities.Add(new HUPriority { Name = "Alta" });
                _context.HUPriorities.Add(new HUPriority { Name = "Media" });
                _context.HUPriorities.Add(new HUPriority { Name = "Baja" });
                _context.HUPriorities.Add(new HUPriority { Name = "Urgente" });
                _context.HUPriorities.Add(new HUPriority { Name = "Crítica" });
                _context.HUPriorities.Add(new HUPriority { Name = "Normal" });
                _context.HUPriorities.Add(new HUPriority { Name = "Importante" });
                _context.HUPriorities.Add(new HUPriority { Name = "Inmediata" });
                _context.HUPriorities.Add(new HUPriority { Name = "Esencial" });
                _context.HUPriorities.Add(new HUPriority { Name = "Pendiente" });
                _context.HUPriorities.Add(new HUPriority { Name = "Emergente" });
                _context.HUPriorities.Add(new HUPriority { Name = "Relevante" });

                _context.SaveChanges();
            }
        }

        private async Task CheckHUApprobalStatus()
        {
            if (!_context.HUApprovalStatuses.Any())
            {
                //_context.ResetTableIDs("HUApprovalStatuses");
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Pendiente de aprobación" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "En revisión" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Aprobado" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Rechazado" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Aprobación parcial" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Aprobación condicional" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Pendiente de revisión adicional" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "En espera de confirmación" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "En proceso de aprobación" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Aprobación pendiente de firma" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Aprobación finalizada" });
                _context.HUApprovalStatuses.Add(new HUApprovalStatus { Name = "Aprobación automática" });
                _context.SaveChanges();
            }
        }

        private async Task CheckDocTraceabilityType()
        {
            if (!_context.DocTraceabilityTypes.Any())
            {
                //_context.ResetTableIDs("DocTraceabilityTypes");
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Objetivo" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "KPI" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Triada Estructural" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Problema" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Responsabilidad" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Restricción" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Tipo1" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Tipo2" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Tipo3" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Tipo4" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Tipo5" });
                _context.DocTraceabilityTypes.Add(new DocTraceabilityType { Name = "Tipo6" });
                _context.SaveChanges();
            }
        }

        private async Task CheckCitiesAsync()
        {
            if (!_context.Cities.Any())
            {
                //_context.ResetTableIDs("Cities");

                int antioquiaId = _context.States.Single(s => s.Name == "Antioquia").StateId;

                _context.Cities.Add(new City { Name = "Medellin", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Abejorral", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Abriaqui", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Alejandria", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Amaga", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Amalfi", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Andes", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Angelopolis", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Angostura", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Anori", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Santafe De Antioquia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Anza", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Apartado", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Arboletes", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Argelia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Armenia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Barbosa", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Belmira", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Bello", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Betania", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Betulia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Ciudad Bolivar", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Briceño", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Buritica", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Caceres", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Caicedo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Caldas", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Campamento", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Cañasgordas", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Caracoli", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Caramanta", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Carepa", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "El Carmen De Viboral", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Carolina", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Caucasia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Chigorodo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Cisneros", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Cocorna", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Concepcion", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Concordia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Copacabana", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Dabeiba", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Don Matias", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Ebejico", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "El Bagre", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Entrerrios", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Envigado", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Fredonia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Frontino", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Giraldo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Girardota", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Gomez Plata", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Granada", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Guadalupe", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Guarne", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Guatape", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Heliconia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Hispania", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Itagüi", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Ituango", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Jardin", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Jerico", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "La Ceja", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "La Mstrella", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "La Pintada", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "La Union", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Liborina", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Maceo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Marinilla", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Montebello", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Murindo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Mutata", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Nariño", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Necocli", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Nechi", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Olaya", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Peñol", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Peque", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Pueblorrico", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Puerto Berrio", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Puerto Nare", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Puerto Triunfo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Remedios", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Retiro", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Rionegro", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Sabanalarga", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Sabaneta", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Salgar", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Andres", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Carlos", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Francisco", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Jeronimo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Jose De La Montaña", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Juan De Uraba", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Luis", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Pedro", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Pedro De Uraba", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Rafael", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Roque", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "San Vicente", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Santa Barbara", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Santa Rosa De osos", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Santo Domingo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "El Santuario", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Segovia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Sonson", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Sopetran", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Tamesis", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Taraza", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Tarso", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Titiribi", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Toledo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Turbo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Uramita", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Urrao", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Valdivia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Valparaiso", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Vegachi", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Venecia", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Vigia Del Fuerte", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Yali", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Yarumal", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Yolombo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Yondo", StateId = antioquiaId });
                _context.Cities.Add(new City { Name = "Zaragoza", StateId = antioquiaId });
                SaveDBChanges();
            }
        }

        private async Task CheckStatesAsync()
        {
            if (!_context.States.Any())
            {
                //_context.ResetTableIDs("States");
                int colombiaId = _context.Countries.Single(c => c.Name == "Colombia").CountryId;

                _context.States.Add(new State { Name = "Antioquia", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Atlántico", CountryId = colombiaId });
                _context.States.Add(new State { Name = "D.C.", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Bolívar", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Boyacá", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Caldas", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Caquetá", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Cauca", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Cesar", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Córdoba", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Cundinamarca", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Chocó", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Huila", CountryId = colombiaId });
                _context.States.Add(new State { Name = "La Guajira", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Magdalena", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Meta", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Nariño", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Norte de Santander", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Quindio", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Risaralda", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Santander", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Sucre", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Tolima", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Valle del Cauca", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Arauca", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Casanare", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Putumayo", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Archipiélago de San Andrés, Providencia y Santa Catalina", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Amazonas", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Guainía", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Guaviare", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Vaupés", CountryId = colombiaId });
                _context.States.Add(new State { Name = "Vichada", CountryId = colombiaId });
                _context.SaveChanges();
            }
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                //_context.ResetTableIDs("Countries");
                _context.Countries.Add(new Shared.Entities.Country { Name = "Colombia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Argentina" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Brasil" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Chile" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Perú" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "México" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "España" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Francia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Italia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Alemania" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Canadá" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Estados Unidos" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Japón" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Corea del Sur" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Australia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Nueva Zelanda" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "India" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "China" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Rusia" });
                _context.Countries.Add(new Shared.Entities.Country { Name = "Sudáfrica" });
                _context.SaveChanges();
            }
        }

        public void SaveDBChanges()
        {
            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine("Error al guardar los cambios en la base de datos: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }
    }
}
