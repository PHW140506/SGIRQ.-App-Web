import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../core/auth.service';
@Component({ selector: 'app-shell', standalone: true,
  imports: [RouterLink, RouterLinkActive, RouterOutlet], templateUrl: './shell.component.html' })
export class ShellComponent { constructor(public auth: AuthService) {} }
