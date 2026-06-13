export interface DetectResponse {
  id: number;
  inputText: string;

  language?: string;
  score?: number;
  confidence?: number;

  languageBreakdown?: {
    [key: string]: number;
  };

  segments?: any;

  explanation?: string;
  suggestions?: string;
  improvedText?: string;

  createdAt?: string;
}