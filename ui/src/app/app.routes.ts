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
