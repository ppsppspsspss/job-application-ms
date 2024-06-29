export interface JobApplication{
    jobApplicationID: number
    jobID: number
    firstName: string
    lastName: string
    fathersName: string
    mothersName: string
    phone: string
    email: string
    currentAddress: string
    permanentAddress: string
    bscStatus: string
    bscAdmissionDate: string
    bscGraduationDate: string
    bscAIUB: string
    bscAIUBID: string
    bscCGPA: string
    bscGraduate: string
    bscUniversity: string
    mscStatus: string
    mscAdmissionDate: string
    mscGraduationDate: string
    mscAIUB: string
    mscAIUBID: string
    mscCGPA: string
    mscGraduate: string
    mscUniversity: string
    cv?: File | string
    coverLetter?: File | string
}