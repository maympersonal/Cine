import React, { useEffect, useState } from "react";
import axios from '../../api/axios';
import MovieCarousel from "./MovieCarousel";

const MovieCarouselContainer = () => {
    const [slides, setSlides] = useState([]);

    useEffect(() => {
        const fetchMovies = async () => {
            try {
                const res = await axios.get('Pelicula/GetTop');
                const fetchedMovies = res.data;

                const moviesWithAdditionalDetails = await Promise.all(fetchedMovies.map(async movie => {
                    return {
                        ...movie,
                        genres: await Promise.all(movie.idGs.map(async idG => {
                            const genreResponse = await axios.get(`Genero/GetById/${idG}`);
                            return genreResponse.data.nombreG;
                        })),
                        actors: await Promise.all(movie.idAs.map(async idA => {
                            const actorResponse = await axios.get(`Actor/GetById/${idA}`);
                            return actorResponse.data.nombreA;
                        })),
                    };
                }));

                setSlides(moviesWithAdditionalDetails.slice(0, 15));
            } catch (err) {
                console.error('Error fetching top movies:', err);
            }
        };

        fetchMovies();
    }, []);

    return (
        <div>
            {slides && slides.length > 0 &&
                <MovieCarousel slides={slides} controls indicators />
            }
        </div>
    );
};

export default MovieCarouselContainer;
