using WebTutor.Medels;

namespace WebTutor.Services.Intrerfaces
{
    public interface IPostServices
    {
        Authorization Create(Authorization Authorization);
        Authorization Update(Authorization Authorization);
        Authorization Get(int id);
        //List<Authorization> GetAll();
        void Delete(int id);
        public bool Check(Authorization aut);
    }
}
