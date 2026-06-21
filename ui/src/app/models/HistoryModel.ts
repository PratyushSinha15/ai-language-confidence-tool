export interface HistoryItem {
  id: number;
  inputText: string;
  language: string | null;
  score: number | null;
  confidence: number | null;
  languageBreakdown: any;
  segments: any;
  explanation: string | null;
  suggestions: string | null;
  improvedText: string | null;
  createdAt: string | null;
}