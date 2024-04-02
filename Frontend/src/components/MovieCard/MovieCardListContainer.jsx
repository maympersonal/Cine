import React, { useEffect, useState } from "react";
import Loader from "../Loader/Loader";
import MovieCardList from './MovieCardList';
import axios from '../../api/axios'; // Asegúrate de que esta ruta sea correcta

const MovieCardListContainer = () => {
    const [loading, setLoading] = useState(true);
    const [movies, setMovies] = useState([]);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const { data } = await axios.get('/api/Pelicula/GetAll');
                setMovies(data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching movies:', error);
                setLoading(false);
            }
        };
        fetchMovies();
    }, []);

    // Filtrar por año actual para "Próximos estrenos"
    const currentYear = new Date().getFullYear();
    const upcomingMovies = movies.filter(movie => movie.anno === currentYear);

    return (
        <div>
            {!loading ? (
                <>
                    <MovieCardList movies={movies} listTitle="En Cartelera" />
                    <MovieCardList movies={upcomingMovies} listTitle="Próximos Estrenos" />
                </>
            ) : <Loader />}
        </div>
    );
};

export default MovieCardListContainer;
