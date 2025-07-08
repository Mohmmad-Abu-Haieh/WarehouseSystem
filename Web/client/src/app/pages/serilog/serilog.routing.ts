import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogsComponent } from './serilog.component';

const routes: Routes = [
    {
        path: '',
        component: LogsComponent,
    },  
];
@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class LogsRoutingModule {}
