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

        CrudEventViewModel GetCrudEventViewModelById(string eventId);

        CrudEventViewModel AddEvent(CrudEventViewModel crudEventViewModel);

        void EditEvent(CrudEventViewModel crudEventViewModel);

        void DeleteEvent(string eventId);
    }
}
