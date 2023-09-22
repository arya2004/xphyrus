export interface IAssesmentAdminHome {
    assesmentId: string;
    name?: string;
    description?: string;
    code?: string;
    isStrict: boolean;
    creationDate: Date;
    startDate: Date;
    endDate: Date;
    duration: Date;
    codings?: string;
  }