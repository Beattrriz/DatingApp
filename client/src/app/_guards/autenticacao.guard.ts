import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

//acesso a rota de autenticação, verificar se o usuario está logado ou não
export const autenticacaoGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);
  
  return accountService.currentUser$.pipe(
    map(usuario => {
      if (usuario) return true;
      else {
        toastr.error('Você não tem acesso!')
        return false;
      }
    })
  )
};
