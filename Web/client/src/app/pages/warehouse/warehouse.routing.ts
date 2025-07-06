import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WarehouseComponent } from './warehouse.component';
import { ItemsWarehouseComponent } from './items-warehouse/items-warehouse.component';

const routes: Routes = [
    {
        path: '',
        component: WarehouseComponent,
    },  
    {
        path: 'items/:id',
        component: ItemsWarehouseComponent
    }
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class WarehouseRoutingModule {}
