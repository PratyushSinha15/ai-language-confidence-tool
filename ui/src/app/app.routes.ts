import { Routes } from '@angular/router';
import { Home } from './features/home/home';
import { Analysis } from './features/analysis/analysis';
import { History } from './features/history/history';

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
    component: Analysis
  },
  {
    path: 'history',
    component: History
  },
  {
    path: '**',
    redirectTo: ''
  }
];
