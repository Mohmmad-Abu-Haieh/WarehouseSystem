import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root'
})

export class HomeService {

    achievementModal = false;
    userDetails: any;
    userId: any;
    resetUserId: any;
    createUserModal = false;
    changePassUserModal = false;
    filterForm: any = false;
    isFilterdUser = false;
    restTemplete: any = false;
    public roles: any;
    public securityGroups: any;
    public locations :any;
    
    userFilter = {
        keyword: "",
        direction: 'DESC',
        orderBy: '',
        pageSize: 10,
        pageIndex: 0,
        roleId: null,
        securityGroupId: null,
        active: false,
        hold:false,
        domainUser: 3
    };

    constructor(public http: HttpClient) {
    }

        
}



