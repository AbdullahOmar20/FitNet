import { Component, inject, ViewEncapsulation } from '@angular/core';
import {MatBadge} from '@angular/material/badge';
import {MatIcon} from '@angular/material/icon';
import {MatButton} from '@angular/material/button';
import { MatProgressBar } from '@angular/material/progress-bar';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { BusyService } from '../../core/services/busy.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    MatBadge,
    MatIcon,
    MatButton,
    RouterLink,
    RouterLinkActive,
    MatProgressBar
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent {
  busySrevice = inject(BusyService)
}
