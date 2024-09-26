using System.Collections.Generic;
using System.Threading.Tasks;


namespace Note.Function.Repository
{
    public interface INoteRepository
    {
        Task<IEnumerable<Entities.PersonalNote>> GetAsync();
        Task<Entities.PersonalNote> GetAsync(string id);

        Task<Entities.PersonalNote> CreateAsync(Entities.PersonalNote article);
        Task<Entities.PersonalNote> UpdateAsync(string id, Entities.PersonalNote note);

        Task DeleteAsync(string id);
    }
}
