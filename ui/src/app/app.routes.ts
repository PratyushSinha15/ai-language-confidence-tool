import { Routes } from '@angular/router';
import { Home } from './features/home/home';
import { Analysis } from './features/analysis/analysis';
import { History } from './features/history/history';
import { authGuard } from './core/guards/auth-guard';

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
          loadComponent:()=>import('./features/auth/login/login')
          .then(m=>m.Login)
        },
        {
          path:'register',
          loadComponent:()=>import('./features/auth/register/register')
          .then(m=>m.Register)
        }
      ]
  },
  {
    path: 'analysis',
    canActivate:[authGuard],
    loadComponent:()=>import('./features/analysis/analysis')
          .then(m=>m.Analysis)
  },
  {
    path: 'history',
    canActivate:[authGuard],
    loadComponent:()=>import('./features/history/history')
          .then(m=>m.History)
  },
  {
    path: '**',
    redirectTo: ''
  }
];
