import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  loading = false;
  counter = 0;

  busy(){
    this.counter++;
    this.loading = true;
  }

  idle(){
    this.counter--;
    if(this.counter <= 0){
      this.counter = 0;
      this.loading = false
    }
  }
}
