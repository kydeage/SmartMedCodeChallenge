using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartMedCodeChallenge.Data
{
    public interface IContext<T> where T : class
    {
        Task<ActionResult<List<T>>> GetAll();
        Task<ActionResult<T>> Get(long id);
        Task<ActionResult<T>> Post(T item);
        Task<IActionResult> Delete(long id);
    }
}
