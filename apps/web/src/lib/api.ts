import axios from 'axios';

// VITE_API_URL is only needed for production builds; dev uses the Vite proxy
const api = axios.create({ baseURL: (import.meta.env.VITE_API_URL as string | undefined) ?? '/api' });

api.interceptors.response.use(
  r => r,
  error => {
    if (error.response?.status === 401) {
      // TODO: redirect to /login once auth UI is built
      console.warn('Unauthorized — authentication required');
    }
    return Promise.reject(error);
  }
);

export type Difficulty = 'Beginner' | 'Intermediate' | 'Advanced' | 'Expert';

export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  page: number;
  pageSize: number;
  totalPages: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}

export interface ChallengeDto {
  id: string;
  title: string;
  description: string;
  difficulty: Difficulty;
  categoryName: string;
  tags: string[];
}

export interface ChallengeImplementationDto {
  id: string;
  dialectId: string;
  dialectName: string;
  languageName: string;
  starterCode: string;
  referenceSolution: string;
  syntaxFeatures: string[];
}

export interface ChallengeDetailsDto {
  id: string;
  title: string;
  description: string;
  difficulty: Difficulty;
  categoryName: string;
  implementations: ChallengeImplementationDto[];
  tags: string[];
}

export interface SubmitChallengeRequest {
  dialectId: string;
  code: string;
}

export interface SubmissionResultDto {
  submissionId: string;
  status: string;
}

export const challengesApi = {
  getAll: (params?: { page?: number; pageSize?: number; difficulty?: Difficulty }) =>
    api.get<PaginatedResult<ChallengeDto>>('/challenges', { params }).then(r => r.data),
  getById: (id: string) =>
    api.get<ChallengeDetailsDto>(`/challenges/${id}`).then(r => r.data),
  submit: (id: string, request: SubmitChallengeRequest) =>
    api.post<SubmissionResultDto>(`/challenges/${id}/submissions`, request).then(r => r.data),
};
