
import React from 'react';

export default function Atividade(props) {
    function prioridadeLabel(param) {
        switch (param) {
            case 'Baixa':
                return 'Baixa';
            case 'Normal':
                return 'Normal';
            case 'Alta':
                return 'Alta';
            default:
                return 'Não definido';
        }
    }

    function prioridadeStyle(param, icone) {
        switch (param) {
            case 'Baixa':
                return icone ? 'smile' : 'success';
            case 'Normal':
                return icone ? 'meh' : 'dark';
            case 'Alta':
                return icone ? 'frown' : 'warning';
            default:
                return 'Não definido';
        }
    }

    return (
        <div
            className={
                'card mb-2 shadow-sm border-' + prioridadeStyle(props.ativ.prioridade)
            }
        >
            <div className='card-body'>
                <div className='d-flex justify-content-between'>
                    <h5 className='card-title'>
                        <span className='badge bg-secondary me-1'>
                            {props.ativ.id}
                        </span>
                        - {props.ativ.titulo}
                    </h5>
                    <h6>
                        Prioridade:
                        <span
                            className={
                                'ms-1 text-' + prioridadeStyle(props.ativ.prioridade)
                            }
                        >
                            <i
                                className={
                                    'me-1 far fa-' +
                                    prioridadeStyle(props.ativ.prioridade, true)
                                }
                            ></i>
                            {prioridadeLabel(props.ativ.prioridade)}
                        </span>
                    </h6>
                </div>
                <p className='card-text'>{props.ativ.descricao}</p>
                <div className='d-flex justify-content-end pt-2 m-0 border-top'>
                    <button 
                        className='btn btn-sm btn-outline-primary me-2'
                        onClick={() => props.pegarAtividade(props.ativ.id)}
                    >
                        <i className='fas fa-pen me-2'></i>
                        Editar
                    </button>
                    <button
                        className='btn btn-sm btn-outline-danger'
                        onClick={() => props.handleConfirmModal(props.ativ.id)}
                    >
                        <i className='fas fa-trash me-2'></i>
                        Deletar
                    </button>
                </div>
            </div>
        </div>
    );
}
