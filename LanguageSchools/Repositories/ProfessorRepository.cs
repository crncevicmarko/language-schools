﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using LanguageSchools.Models;

namespace LanguageSchools.Repositories
{
    class ProfessorRepository : IProfessorRepository, IFilePersistence
    {
        private static List<Professor> professors = new List<Professor>();

        public void Add(Professor professor)
        {
            professors.Add(professor);
            Save();
        }

        public void Add(List<Professor> newProfessors)
        {
            professors.AddRange(newProfessors);
            Save();
        }

        public void Set(List<Professor> newProfessors)
        {
            professors = newProfessors;
        }

        public void Delete(string email)
        {
            Professor professor = GetById(email);

            if (professor != null)
            {
                //professor.User.IsActive = false;
                professors.Remove(professor);
            }

            Save();
        }
        //public void Delete(string email)
        //{
        //    Professor professor = GetById(email);

        //    if (professor != null)
        //    {
        //        professor.User.IsActive = false;
        //    }

        //    Save();
        //}

        public List<Professor> GetAll()
        {
            return professors;
        }

        public Professor GetById(string email)
        {
            return professors.Find(u => u.User.Email == email);
        }

        public void Update(string email, Professor updatedProfessor)
        {
            Save();
        }

        public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            using (Stream stream = new FileStream(Config.professorsFilePath, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, professors);
            }
        }

        public List<User> Search(string sting)
        {
                string lowerTerm = sting.ToLower();
            return Data.Instance.UserService.GetAll().Where(p => (p.FirstName.ToLower().Contains(lowerTerm)
            || p.LastName.ToLower().Contains(lowerTerm)) && !p.IsActive).ToList();
        }
    }
}
