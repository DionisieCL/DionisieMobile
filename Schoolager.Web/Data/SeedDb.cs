using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Schoolager.Web.Data.Entities;
using Schoolager.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Schoolager.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IConverterHelper _converterHelper;

        public SeedDb(
            DataContext context, 
            IUserHelper userHelper,
            IConverterHelper converterHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _converterHelper = converterHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Employee");
            await _userHelper.CheckRoleAsync("Student");
            await _userHelper.CheckRoleAsync("Teacher");

            await AddAdminUser();

            if (!_context.Turmas.Any())
            {
                await AddTurmas();
            }

            if (!_context.Subjects.Any())
            {
                await AddSubjects();
            }

            if (!_context.Teachers.Any())
            {
                await AddTeachers();
            }

            if (!_context.TeacherTurmas.Any()) 
            {
                await AddTeacherTurmas();
            }

            if (!_context.Students.Any())
            {
                await AddStudents();
            }

            if (!_context.Rooms.Any())
            {
                await AddRooms();
            }
        }

        private async Task AddRooms()
        {
            // Load data from json file
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\MockData", "Rooms.json");

            string roomsJson = File.ReadAllText(path);

            List<Room> rooms = JsonConvert.DeserializeObject<List<Room>>(roomsJson);

            foreach (var room in rooms)
            {
                _context.Rooms.Add(room);
            }

            await _context.SaveChangesAsync();
        }

        private async Task AddTurmas()
        {
            // Load data from json file
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\MockData", "Turmas.json");

            string turmasJson = File.ReadAllText(path);

            List<Turma> turmas = JsonConvert.DeserializeObject<List<Turma>>(turmasJson);

            foreach (var turma in turmas)
            {
                _context.Turmas.Add(turma);
            }

            await _context.SaveChangesAsync();
        }


        private async Task AddSubjects()
        {
            // Load data from json file
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\MockData", "Subjects.json");

            string subjectsJson = File.ReadAllText(path);

            List<Subject> subjects = JsonConvert.DeserializeObject<List<Subject>>(subjectsJson);

            foreach (var subject in subjects)
            {
                _context.Subjects.Add(subject);
            }

            await _context.SaveChangesAsync();
        }

        private async Task AddTeachers()
        {
            // Load data from json file
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\MockData", "Teachers.json");

            string teachersJson = File.ReadAllText(path);

            List<Teacher> teachers = JsonConvert.DeserializeObject<List<Teacher>>(teachersJson);

            foreach (var teacher in teachers)
            {
                // if the user does not exist we create a new one
                User user = await _userHelper.GetUserByEmailAsync(teacher.Email);

                if (user == null)
                {
                    user = _converterHelper.ToUser(teacher, new User(), "teachers");

                    var result = await _userHelper.AddUserAsync(user, "123456");

                    // check if adding user was successful
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Could not add user");
                    }

                    await _userHelper.AddUserToRoleAsync(user, "Student");

                    var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                    await _userHelper.ConfirmEmailAsync(user, token);
                }

                teacher.User = user;

                _context.Teachers.Add(teacher);
            }

            await _context.SaveChangesAsync();
        }

        private async Task AddTeacherTurmas()
        {
            List<TeacherTurma> teacherTurmas = new List<TeacherTurma>();

            var teachers = await _context.Teachers.ToListAsync();
            var turmas = await _context.Turmas.ToListAsync();

            foreach (var teacher in teachers)
            {
                foreach(var turma in turmas)
                {
                    teacherTurmas.Add(new TeacherTurma
                    {
                        TeacherId = teacher.Id,
                        TurmaId = turma.Id,
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        private async Task AddAdminUser()
        {
            var user = await _userHelper.GetUserByEmailAsync("admin@schoolager.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "admin@schoolager.com",
                    UserName = "admin@schoolager.com",
                    PhoneNumber = "214658973",
                    PasswordChanged = true,
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder.");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
                await _userHelper.AddUserToRoleAsync(user, "Employee");

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                await _userHelper.ConfirmEmailAsync(user, token);

                await _context.SaveChangesAsync();
            }
        }


        private async Task AddStudents()
        {
            // Load data from json file
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"Data\\MockData", "Students.json");

            string studentsJson = File.ReadAllText(path);

            List<Student> students = JsonConvert.DeserializeObject<List<Student>>(studentsJson);

            foreach (var student in students)
            {

                // if the user does not exist we create a new one
                User user = await _userHelper.GetUserByEmailAsync(student.Email);

                if (user == null)
                {
                    user = _converterHelper.ToUser(student, new User(), "students");

                    var result = await _userHelper.AddUserAsync(user, "123456");

                    // check if adding user was successful
                    if (result != IdentityResult.Success)
                    {
                        throw new InvalidOperationException("Could not add user");
                    }

                    await _userHelper.AddUserToRoleAsync(user, "Student");

                    var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);

                    await _userHelper.ConfirmEmailAsync(user, token);
                }

                student.User = user;

                _context.Students.Add(student);
            }

            await _context.SaveChangesAsync();
        }
    }
}
