import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class MessagesService {
    messages: string[] = [];

    addMessage(message: string) {
        this.messages.push(message);
        if (this.messages.length > 100) {
            this.messages = this.messages.splice(0, 1);
        }
    }

    addMessages(messages: string[]) {
        this.messages.push(...messages);
        if (this.messages.length > 100) {
            this.messages = this.messages.splice(0, this.messages.length - 100);
        }
    }

    addErrorMessages(messages: string[]) {
        this.messages.push(...messages);
        if (this.messages.length > 100) {
            this.messages = this.messages.splice(0, this.messages.length - 100);
        }
    }
}
