import { useState } from "react";
import Swal from "sweetalert2";

const MovieAddForm = ({ onSubmit, close, children }) => {

    const [sinopsis, setSinopsis] = useState();
    const [anno,setAnno] = useState();
    const [nacionalidad, setNacionalidad] = useState();
    const [duracion, setDuracion] = useState();
    const [titulo, setTitulo] = useState();
    const [imagen, setImagen] = useState();
    const [trailer, setTrailer]= useState();
    const [artistas, setArtistas]= useState([]);
    const [generos, setGeneros]= useState([]);
    

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        switch (name) {
            case 'sinopsis':
                setSinopsis(value);
                break;
            case 'anno':
                setAnno(value);
                break;
            case 'nacionalidad':
                setNacionalidad(value);
                break;
            case 'duracion':
                setDuracion(value);
                break;
            case 'titulo':
                setTitulo(value);
                break;
            case 'imagen':
                setImagen(value);
                break;
            case 'trailer':
                setTrailer(value);
                break;
            case 'artistas':
                setArtistas(value);
                break;
            case 'generos':
                setGeneros(value);
                break;
        }
    }

    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })

    const submitRegistration = (e) => {
        e.preventDefault();
        
            const pelicula = {
                sinopsis: sinopsis,
                anno: anno,
                nacionalidad: nacionalidad,
                duración: duracion ,
                titulo: titulo,
                imagen: imagen,
                trailer: trailer,
                idAs: [
                    artistas
                ],
                idGs: [
                    generos
                ]
            }

            onSubmit(pelicula, close);
    }
    

    return (
        <div>
            <div className="flex flex-col items-center sm:justify-center">
                <div className="flex justify-between w-full">
                    <h3 className="text-4xl font-bold text-slate-900  ">
                        Añadir Pelicula
                    </h3>
                    {children}
                </div>

                <form className="w-full flex flex-col gap-3 items-center px-6 py-4 mt-6 overflow-hidden bg-white shadow-md sm:max-w-md sm:rounded-lg text-black" onSubmit={submitRegistration}>
                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="sinopsis" className="label block text-sm font-medium text-gray-700 undefined">
                            <span className="label-text">Sinopsis</span>
                        </label>

                        <input type="text" name="sinopsis" className="input input-sm input-bordered w-full max-w-xs" autoComplete="given-name" required onChange={handleInputChange} />
                    </div>

                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="anno" className="label block text-sm font-medium text-gray-700 undefined">
                            <span className="label-text">Año</span>
                        </label>

                        <input type="number" name="anno" className="input input-sm input-bordered w-full max-w-xs" autoComplete="family-name" required onChange={handleInputChange} />
                    </div>
                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="nacionalidad" className="label block text-sm font-medium text-gray-700 undefined">
                            <span className="label-text">Año</span>
                        </label>

                        <input type="text" name="nacionalidad" className="input input-sm input-bordered w-full max-w-xs" autoComplete="family-name" required onChange={handleInputChange} />
                    </div>

                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="duracion" className="label block text-sm font-medium text-gray-700 undefined">
                            Duracion
                        </label>

                        <input type="number" name="duracion" className="input input-sm input-bordered w-full max-w-xs" autoComplete="email" required onChange={handleInputChange} />
                    </div>
                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="titulo" className="label block text-sm font-medium text-gray-700 undefined">
                            Titulo
                        </label>

                        <input type="text" name="titulo" className="input input-sm input-bordered w-full max-w-xs" autoComplete="CI" required onChange={handleInputChange} />
                    </div>

                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="imagen" className="label block text-sm font-medium text-gray-700 undefined">
                            Imagen
                        </label>

                        <input type="text" name="imagen" className="input input-sm input-bordered w-full max-w-xs" required onChange={handleInputChange} />
                    </div>

                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="trailer" className="label block text-sm font-medium text-gray-700 undefined">
                            Trailer
                        </label>

                        <input type="text" name="trailer" className="input input-sm input-bordered w-full max-w-xs" required onChange={handleInputChange} />
                    </div>
                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="artistas" className="label block text-sm font-medium text-gray-700 undefined">
                            Artistas
                        </label>

                        <input type="text" name="artistas" className="input input-sm input-bordered w-full max-w-xs" required onChange={handleInputChange} />
                    </div>
                    <div className="form-control w-full max-w-xs">
                        <label htmlFor="generos" className="label block text-sm font-medium text-gray-700 undefined">
                            Generos
                        </label>

                        <input type="text" name="generos" className="input input-sm input-bordered w-full max-w-xs" required onChange={handleInputChange} />
                    </div>

                    <button type="submit" className="btn btn-wide font-semibold tracking-widest text-white uppercase mt-6">Registrase</button>

                </form>
            </div>
        </div>
    );

}
export default MovieAddForm;