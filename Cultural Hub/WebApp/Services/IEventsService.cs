using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Services
{
    public interface IEventsService
    {
        List<EventDetailsViewModel> GetEventDetailsList();

        List<EventShortInfoViewModel> GetEventShortInfoList();

        EventDetailsViewModel GetEventDetailsById(string id);

        CrudEventViewModel AddEvent(CrudEventViewModel CrudEventViewModel);

        CrudEventViewModel EditEvent(CrudEventViewModel CrudEventViewModel);

        void DeleteEvent(CrudEventViewModel CrudEventViewModel);
    }
}
