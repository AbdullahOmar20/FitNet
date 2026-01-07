import { InteractivityChecker } from '@angular/cdk/a11y';
import { Component, EventEmitter, Input, Output, OnDestroy, OnInit, input, output} from '@angular/core';
import { Subject, Subscription } from 'rxjs';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
@Component({
    selector: 'app-search-input',
    imports: [],
    templateUrl: './app-search-input.component.html',
    styleUrl: './app-search-input.component.scss'
})
export class AppSearchInputComponent implements OnInit, OnDestroy{
  
  @Input() InitiateValue: string = ''
  @Input() DebounceValue: number = 300

  @Output() textChange = new EventEmitter<string>()

  inputValue = new Subject<string>();

  trigger = this.inputValue.pipe(
    debounceTime(this.DebounceValue),
    distinctUntilChanged()
  );

  subscriptions: Subscription[] = []

  ngOnInit(): void {
    const subs = this.trigger.subscribe(currentValue => {
      this.textChange.emit(currentValue)
    });
    this.subscriptions.push(subs)
  }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  onInput(e: any){
    this.inputValue.next(e.target.value)
  }

}
