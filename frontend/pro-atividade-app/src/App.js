import { useState, useEffect } from 'react';
import './App.css';
import {Button, Modal, ModalFooter} from 'react-bootstrap'
import AtividadeForm from './components/AtividadeForm';
import AtividadeLista from './components/AtividadeLista';
import api from './api/atividade';

function App() {
    const [showAtividadeModal, setShowAtividadeModal] = useState(false);
    const [smShowConfirmModal, setSmShowConfirmModal] = useState(false);
    
    const [atividades, setAtividades] = useState([]);
    const [atividade, setAtividade] = useState({ id: 0 });

    const handleAtividadeModal = () =>
        setShowAtividadeModal(!showAtividadeModal);

    const handleConfirmModal = () =>
        setSmShowConfirmModal(!smShowConfirmModal);

    const pegaTodasAtividades = async () => {
        const response = await api.get('atividade')
        return response.data;
    }

    const novaAtividade = () =>{
        setAtividade({ id: 0 });
        handleAtividadeModal ();
    }

    useEffect(() => {
       const getAtividades = async () => {
        const todasAtividades = await pegaTodasAtividades();
        if (todasAtividades) setAtividades (todasAtividades);
       };
       getAtividades()
    }, []);

    const addAtividade = async (ativ) => {
        const response = await api.post('atividade', ativ)
        console.log(response.data);
        setAtividades([...atividades, response.data]);
        handleAtividadeModal ();
    };

    function cancelarAtividade() {
        setAtividade({ id: 0 });
        handleAtividadeModal ();
    }

    const atualizarAtividade = async(ativ) => {
        const response = await api.put(`atividade/${ativ.id}`, ativ);
        const  {id} =  response.data;
        setAtividades(
            atividades.map((item) => (item.id === id ? response.data : item))
        );
        setAtividade({ id: 0 });
        handleAtividadeModal ();
    }

    const deletarAtividade = async (id) => {
        if (await api.delete(`atividade/${id}`)){
            const atividadesFiltradas = atividades.filter(
                (atividade) => atividade.id !== id
            );
            setAtividades([...atividadesFiltradas]);
            handleAtividadeModal ();
        }
        
    }

    function pegarAtividade(id) {
        const atividade = atividades.filter((atividade) => atividade.id === id);
        setAtividade(atividade[0]);
        handleAtividadeModal ();
    }

    return (
        <>
            <div className='d-flex justify-content-between align-items-end mt-2 pb-3 border-bottom border-1' >
                <h1 className='m-0 p-0'>
                    Atividade {atividade.id !== 0 ? atividade.id : ''}
                </h1>
                <Button variant="outline-secondary" onClick={novaAtividade}>
                    <i className='fas fa-plus'></i>
                </Button>
            </div>
            
            <AtividadeLista
                atividades={atividades}
                pegarAtividade={pegarAtividade}
                handleConfirmModal = {handleConfirmModal}
            />

                <Modal show={showAtividadeModal} onHide={handleAtividadeModal}>
                    <Modal.Header closeButton>
                    <Modal.Title>Atividade {atividade.id !== 0 ? atividade.id : ''}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                    <AtividadeForm
                        addAtividade={addAtividade}
                        cancelarAtividade={cancelarAtividade}
                        atualizarAtividade={atualizarAtividade}
                        ativSelecionada={atividade}
                        atividades={atividades}
                    />
                    </Modal.Body>
                </Modal>

                <Modal
                     size='sm'
                     show={smShowConfirmModal} 
                    >

                    <Modal.Header closeButton>
                    <Modal.Title>
                        Excluindo Atividade{''}                        
                        {atividade.id !== 0 ? atividade.id : ''}</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                        Tem  certeza que deseja deletar a Atividade {atividade.id} ?
                    </Modal.Body>
                    <ModalFooter className='d-flex justify-content-between'>
                        <button className='btn btn-outline-succsses me-2' onClick={() => deletarAtividade(atividade.id)}>
                            <i className='fas fa-check me-2'></i>
                            Sim
                        </button>
                        <button className='btn btn-outline-danger me-2' onClick={() => handleConfirmModal}>
                            <i className='fas fa-times me-2'></i>
                            Não
                        </button>
                    </ModalFooter>
                </Modal>
        </>
    );
}

export default App;
