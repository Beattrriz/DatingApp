import { CanDeactivateFn } from '@angular/router';
import { EditarMembroComponent } from '../membros/editar-membro/editar-membro.component';

export const evitarAlteracoesNaoSalvasGuard: CanDeactivateFn<EditarMembroComponent> = (component) => {
  if (component.editForm?.dirty) {
    return confirm('Deseja continuar? Quaisquer alterações não salvas serão perdidas!')
  }
  
  return true;
};
