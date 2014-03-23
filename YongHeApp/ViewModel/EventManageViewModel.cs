using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Input;
using BAL;
using BaseLibrary;
using GalaSoft.MvvmLight.Command;
using Model;

namespace YongHeApp.ViewModel
{
    public class EventManageViewModel : ViewModelBase, IDataErrorInfo
    {
        public ObservableCollection<EventInfo> EventList { get; set; }

        public ICommand AddEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }

        public EventManageViewModel()
        {
            EventList=new ObservableCollection<EventInfo>(EventManager.Instance.GetList().Distinct());
            this.AddEventCommand=new RelayCommand<string>((newEvent)=>
                                                              {
                                                                  var existed=this.EventList.Any(p => p.Event == newEvent.Trim());
                                                                  if (existed)
                                                                  {
                                                                      //TODO 显示界面
                                                                      return;
                                                                  }

                                                                  EventInfo addedEvent = new EventInfo()
                                                                                             {
                                                                                                 Event = newEvent.Trim()
                                                                                             };
                                                                  int id=EventManager.Instance.Add(addedEvent);
                                                                  if (id==-1)
                                                                  {
                                                                      return;
                                                                  }

                                                                  addedEvent.Id = id;
                                                                  this.EventList.Add(addedEvent);
                                                                  if (EventManager.Instance.OnEventListChanged!=null)
                                                                  {
                                                                      EventManager.Instance.OnEventListChanged();
                                                                  }
                                                                  

                                                              });

            this.DeleteEventCommand=new RelayCommand<EventInfo>((eventInfo)=>
                                                                    {
                                                                        
                                                                        if (EventManager.Instance.Delete(eventInfo.Id))
                                                                        {
                                                                            this.EventList.Remove(eventInfo);
                                                                        }
                                                                        
                                                                    });
            
        }





       
    }
}
