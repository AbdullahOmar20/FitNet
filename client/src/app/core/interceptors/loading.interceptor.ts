import { HttpInterceptorFn } from '@angular/common/http';
import { delay } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    delay(500)
  )
};
