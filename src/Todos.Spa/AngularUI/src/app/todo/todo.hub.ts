import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ITodo } from "./todo.interface";

@Injectable({
  providedIn: 'root'
})
export class TodoHub {
  private hubConnection: HubConnection;

  constructor() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/notesHub') // Replace with your SignalR hub URL
      .build();

    this.hubConnection.start()
      .then(() => console.log('SignalR connection started.'))
      .catch(err => console.error('Error while starting SignalR connection: ', err));
  }

  addMessageListener(callback: (message: ITodo) => void) {
    this.hubConnection.on('NoteChanged', callback);
  }

  sendMessage(message: ITodo) {
    this.hubConnection.invoke('SendMessage', message)
      .catch(err => console.error('Error while sending message: ', err));
  }
}
