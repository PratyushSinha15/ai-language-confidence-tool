import { Routes } from '@angular/router';
import { Home } from './features/home/home.component';
import { Analysis } from './features/analysis/analysis.component';
import { History } from './features/history/history.component';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    component: Home
  },
  {
    path:'auth',

      children:[
        {
          path:'login',
          loadComponent:()=>import('./features/auth/login/login.component')
          .then(m=>m.Login)
        },
        {
          path:'register',
          loadComponent:()=>import('./features/auth/register/register.component')
          .then(m=>m.Register)
        }
      ]
  },
  {
    path: 'analysis',
    canActivate:[authGuard],
    loadComponent:()=>import('./features/analysis/analysis.component')
          .then(m=>m.Analysis)
  },
  {
    path: 'history',
    canActivate:[authGuard],
    loadComponent:()=>import('./features/history/history.component')
          .then(m=>m.History)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
