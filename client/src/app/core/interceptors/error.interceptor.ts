import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { SnackbarService } from '../services/snackbar.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  var router = inject(Router);
  var snackBar = inject(SnackbarService);

  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if(err.status === 400){
        snackBar.error(err.error.title || err.error)      
      }
      if(err.status === 401){
        snackBar.error(err.error.title || err.error)
      }
      if(err.status === 403){

      }
      if(err.status === 404){

      }
      if(err.status === 500){
        const navigationExtras: NavigationExtras = {state: {error: err.error}}
        router.navigateByUrl('/server-error', navigationExtras)
      }
      
      return throwError(() => err)
    }) 
  );
};
