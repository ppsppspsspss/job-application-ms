export interface Result<T> {
    isError: boolean;
    messages: string[];
    data: T | null;  
  }