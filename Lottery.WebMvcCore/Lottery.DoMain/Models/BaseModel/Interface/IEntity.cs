using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Models.BaseModel.Interface
{
    public interface IEntity<Tkey>
    {
        Tkey Id { get; set; }
        bool IsDeleted { get; set; }
    }
}
