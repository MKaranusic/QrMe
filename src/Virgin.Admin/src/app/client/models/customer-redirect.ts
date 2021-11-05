export interface CustomerRedirect {
    id?: number,
    name: string,
    customerId?: string,
    targetUrl: string,
    isActive: boolean,
    timesViewed?: number
}