using WebTutor.Data;
using WebTutor.Medels;
using WebTutor.Services.Intrerfaces;

namespace WebTutor.Services
{
    public class PersonServices : IPostServices
    {
        PersonContext db;
        public PersonServices(PersonContext context)
        {
            db = context;
        }

        public Authorization Create(Authorization Authorization)
        {
            db.Authorization.Add(Authorization);
            db.SaveChanges();
            return db.Authorization.ToList().Last();
        }
        public Authorization Update(Authorization Authorization)
        {
            Authorization? _person = db.Authorization.First(x => x.Id == Authorization.Id);
            _person.Email = Authorization.Email;
            _person.Password = Authorization.Password;
            db.SaveChanges();
            return db.Authorization.First(x => x.Id == Authorization.Id);
        }

        public Authorization Get(int id)
        {
            Authorization? _person = null;
            foreach (Authorization item in db.Authorization.ToList())
            {
                if (item.Id == id)
                {
                    _person = item;
                    break;
                }
            }
            return _person;
        }
        public void Delete(int id)
        {
            Authorization _person = db.Authorization.First(x => x.Id == id);
            db.Authorization.Remove(_person);
            db.SaveChanges();
            return;
        }
        public bool Check(Authorization aut)
        {
            foreach (Authorization item in db.Authorization)
            {
                if (item.Email == aut.Email && item.Password == aut.Password) { return true; }
            }
            return false;
        }
    }
}
