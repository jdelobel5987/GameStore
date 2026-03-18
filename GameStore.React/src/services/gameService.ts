import type { Game } from '../types/Games';

const API_URL = `${import.meta.env.VITE_API_URL}/games`;

export async function getGames(): Promise<Game[]> {
    const response = await fetch(API_URL);

    if (!response.ok) {
        throw new Error('Failed to fetch games');
    }

    return response.json();
}