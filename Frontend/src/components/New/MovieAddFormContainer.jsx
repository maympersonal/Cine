import MovieAddForm from "./MovieAddForm"
import { useUser } from "../../context/UserContext"

const MovieAddFormContainer = ({ closeForm, open}) => {

    const {createMovie} = useUser();    

    const closeOnClickOutside = (e) => {
        e.target.classList.contains('modal') && closeForm();            
    }
    
    return (
        <div className='w-full flex justify-center px-32 absolute top-0 font-albert'>
            <input type="checkbox" id="userFormModal" className="modal-toggle" checked={open} onChange={() => { }} />
            <div className="modal" onClick={closeOnClickOutside}>

                <div className="modal-box creditCardContainer rounded">
                    <MovieAddForm onSubmit={createMovie} close={closeForm} />
                </div>

            </div>

        </div>
    )
}
export default MovieAddFormContainer