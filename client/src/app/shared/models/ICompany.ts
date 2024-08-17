export interface ICompany {
    companyId: number;
    companyCode?: string | null;
    name: string;
    directorName?: string | null;
    directorEmployeeCode: number;
    directorMobile: number;
    landLine: number;
    directorEmail?: string | null;
    addressLine1?: string | null;
    addressLine2?: string | null;
    addressLine3?: string | null;
    city?: string | null;
    state?: string | null;
    country?: string | null;
    postalCode: number;
    panNumber?: string | null;
}