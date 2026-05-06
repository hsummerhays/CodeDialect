import axios from 'axios';

// VITE_API_URL is only needed for production builds; dev uses the Vite proxy
const api = axios.create({ baseURL: (import.meta.env.VITE_API_URL as string | undefined) ?? '/api' });

export type Difficulty = 'Beginner' | 'Intermediate' | 'Advanced' | 'Expert';

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
  getAll: () => api.get<ChallengeDto[]>('/challenges').then(r => r.data),
  getById: (id: string) => api.get<ChallengeDetailsDto>(`/challenges/${id}`).then(r => r.data),
  submit: (id: string, request: SubmitChallengeRequest) =>
    api.post<SubmissionResultDto>(`/challenges/${id}/submissions`, request).then(r => r.data),
};
