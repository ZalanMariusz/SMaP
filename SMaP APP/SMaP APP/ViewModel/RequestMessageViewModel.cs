using SMaP_APP.Commands;
using SMaP_APP.DAL;
using SMaP_APP.Model;
using SMaP_APP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMaP_APP.ViewModel
{
    class RequestMessageViewModel : BaseViewModel<RequestMessage>
    {
        public RelayCommand SaveCommand { get; set; }
        public RequestMessage SelectedRequestMessage { get; set; }
        public RequestMessageViewModel(RequestMessageWindow window,RequestMessage selectedRequestMessage)
        {
            this._contextDal = new RequestMessageDAL();
            this.SelectedRequestMessage = selectedRequestMessage;
            this.SourceWindow = window;
            this.SaveCommand = new RelayCommand(SaveMessage, CanExecute);
        }

        private void SaveMessage()
        {
            SelectedRequestMessage.Created = DateTime.Now;
            _contextDal.Create(SelectedRequestMessage);
            this.Close();
        }
    }
}
