import './App.css'
import { useEffect, useState } from "react";
import { getGames } from "./services/gameService";
import type { Game } from "./types/Games";

function App() {
  const [games, setGames] = useState<Game[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getGames()
      .then(setGames)
      .catch(err => console.error(err))
      .finally(() => setLoading(false));
  }, []);

  
  return (
    <>
        <div>
          <h1>GameStore API</h1>
          <p>Welcome to the GameStore API!</p>
          
          {loading && <p style={{ color: 'blue'}}>Loading...</p>}
          
          {!loading && games.length === 0 && <p style={{ color: 'orange'}}>No games found.</p>}

          {!loading && games.length > 0 && (
            <table>
              <thead>
                <tr>
                  <th>Name</th>
                  <th>Genre</th>
                  <th>Price</th>
                  <th>Release Date</th>
                </tr>
              </thead>
              <tbody>
                {games.map(game => (
                  <tr key={game.id}>
                    <td>{game.name}</td>
                    <td>{game.genre}</td>
                    <td>${game.price.toFixed(2)}</td>
                    <td>{new Date(game.releaseDate).toLocaleDateString()}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          )}

        </div>
    </>
  )
}

export default App